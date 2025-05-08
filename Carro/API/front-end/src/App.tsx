import React from "react";
import Button from "./components/Button";
import { useState } from "react";
import Header from "./components/Header";

//1 - Um componente SEMPRE deve começar com a primeira letra
//maiúscula
//2 - Todo componente DEVE ser uma função do JS
//3 - Todo deve retornar apenas UM elemento HTML
function App() {
  const [click, setClick] = useState(0);

  function handleClick(){
    setClick(click + 1);
}

  return (
    <div>
      <Header/>
      <h1>Projeto base em React com TypeScript</h1>

      <Button texto="Botão 1" valor={click} evento={handleClick}/>
      <Button texto="Botão 2" valor={click} evento={handleClick}/>
    </div>
  );
}
//4 - OBRIGATORIAMENTE o componente DEVE ser exportado
export default App;