import React, { useState } from "react";
import { Link, useHistory } from "react-router-dom";
import { FiArrowLeft } from "react-icons/fi";
import querystring from "querystring";

import "./styles.css";
import api from "../../services/api";

export default function Registro() {
  const [nome, setNome] = useState("");
  const [login, setLogin] = useState("");
  const [senha, setSenha] = useState("");

  const history = useHistory();

  async function handleCadastrar(e) {
    e.preventDefault();

    const dados = {
      nome,
      login,
      senha,
    };

    try {
      const resposta = await api.post(
        "usuarios",
        querystring.stringify(dados),
        {
          headers: {
            "Content-Type": "application/x-www-form-urlencoded",
          },
        }
      );

      alert(`Seu ID de acesso: ${resposta.data.id}`);

      history.push("/login");
    } catch (erro) {
      const mensagemDeErro = erro.response.data;
      alert(mensagemDeErro);
    }
  }

  return (
    <div className="registro-container">
      <div className="content">
        <Link className="link" to="/login">
          <FiArrowLeft size={16} color="#483d8b" />
          Retornar
        </Link>

        <form onSubmit={handleCadastrar}>
          <input
            placeholder="Nome"
            value={nome}
            onChange={(e) => setNome(e.target.value)}
          />

          <input
            placeholder="Login"
            value={login}
            onChange={(e) => setLogin(e.target.value)}
          />

          <input
            placeholder="Senha"
            type="password"
            value={senha}
            onChange={(e) => setSenha(e.target.value)}
          />

          <button className="button" type="submit">
            Cadastrar
          </button>
        </form>
      </div>
    </div>
  );
}
