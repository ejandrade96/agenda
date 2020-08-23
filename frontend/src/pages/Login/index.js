import React, { useState } from "react";
import { Link, useHistory } from "react-router-dom";
import { FiLogIn } from "react-icons/fi";
import querystring from "querystring";

import api from "../../services/api";
import "./styles.css";
import loginImg from "../../assets/login.png";

export default function Login() {
  const [login, setLogin] = useState("");
  const [senha, setSenha] = useState("");

  const history = useHistory();

  async function handleEntrar(e) {
    e.preventDefault();

    const dados = {
      login,
      senha,
    };

    try {
      const resposta = await api.post("login", querystring.stringify(dados), {
        headers: {
          "Content-Type": "application/x-www-form-urlencoded",
        },
      });

      localStorage.setItem("usuarioId", resposta.data.id);
      localStorage.setItem("usuarioLogin", resposta.data.login);
      localStorage.setItem("usuarioNome", resposta.data.nome);
      localStorage.setItem("usuarioToken", resposta.data.token);

      history.push("/home");
    } catch (erro) {
      const mensagemDeErro = erro.response.data.mensagem;
      alert(mensagemDeErro);
      setLogin("");
      setSenha("");
      history.push("/login");
    }
  }

  return (
    <div className="login-container">
      <section className="form">
        <form onSubmit={handleEntrar}>
          <h1>Faça seu login</h1>

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
            Entrar
          </button>

          <Link className="link" to="/registro">
            <FiLogIn size={16} color="#0000CD" />
            Não tenho cadastro
          </Link>
        </form>
      </section>

      <img src={loginImg} alt="Login" />
    </div>
  );
}
