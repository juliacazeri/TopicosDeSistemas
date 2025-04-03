using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();

var app = builder.Build();

// Endpoints relacionados ao recurso de Carros
// GET: Lista todos os carros cadastrados
app.MapGet("/api/carros", ([FromServices] AppDataContext ctx) => {
    if(ctx.Carros.Any()){
        return Results.Ok(ctx.Carros.ToList());
    }

    return Results.NotFound();
});

// POST: Cadastrar carro
app.MapPost("/api/carros", ([FromBody] Carro carro,
                            [FromServices] AppDataContext ctx) => {
    
    carro.Modelo = ctx.Modelos.Find(carro.Modelo.Id);

    if(carro.Modelo == null){
        return Results.BadRequest("Modelo não existe!");
    }

    if(carro.Name == null || carro.Name.Length < 3){
        return Results.BadRequest("Nome do carro deve conter mais de 3char!");
    }

    ctx.Carros.Add(carro);
    ctx.SaveChanges();
    return Results.Created("", carro);
});

// GET: Buscar o nome pelo ID
app.MapGet("/api/carros/{id}", ([FromRoute] int id,
                               [FromServices] AppDataContext ctx) => {
                                
    Carro? carro = ctx.Carros.Find(id);

    if (carro != null){
       return Results.Ok(carro);
    }

    return Results.NotFound();
});

// PUT: Atualiza os dados do Carro pelo ID
app.MapPut("/api/carros/{id}", ([FromRoute] int id,
                               [FromBody] Carro carro,
                               [FromServices] AppDataContext ctx) => {

    Carro? entidade = ctx.Carros.Find(id);
    entidade.Modelo = ctx.Modelos.Find(carro.Modelo.Id);

     if(entidade.Modelo == null){
        return Results.BadRequest("Modelo não existe!");
    }
    if(carro.Name == null || carro.Name.Length < 3){
        return Results.BadRequest("Nome do carro deve conter mais de 3char!");
    }


    if (entidade != null){
        entidade.Name = carro.Name;
        ctx.Carros.Update(entidade);
        ctx.SaveChanges();
        return Results.Ok(entidade);
    }
    return Results.NotFound();
});

// DELETE: Remove um carro pelo ID
app.MapDelete("/api/carros/{id}", ([FromRoute] int id,
                                 [FromServices] AppDataContext ctx) => {
    
    Carro carro = ctx.Carros.Find(id);
    if(carro == null){
        Results.NotFound();
    }
    
    ctx.Carros.Remove(carro);
    ctx.SaveChanges();
    return Results.NoContent();
});
// GET: Lista todos os modelos cadastrados
app.MapGet("/api/modelos", ([FromServices] AppDataContext ctx) => {
    var modelos = ctx.Modelos.ToList();
    if(modelos == null || modelos.Count == 0){
        return Results.NotFound();
    }
    return Results.Ok(modelos);
}


);

// GET: Busca modelos cadastrados
app.MapGet("/api/modelos/{id}", ([FromRoute] int id,
                                [FromServices] AppDataContext ctx) => {

    var modelo = ctx.Modelos.Find(id);
    if(modelo == null){
        return Results.NotFound();
    }
    return Results.Ok(modelo);
});

app.Run();