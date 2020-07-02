import React, { useState } from "react";
import { Link, useHistory } from "react-router-dom";
import { FiArrowLeft } from "react-icons/fi";

import api from "../../services/api";
import "./styles.css";

export default function Salvar(props) {
  const estado = props.location.state;
  const contatoId = props.match.params["contatoId"];

  const [nome, setNome] = useState(estado ? props.location.state.nome : "");

  const [telefone, setTelefone] = useState(
    estado ? props.location.state.telefone : ""
  );

  const [celular, setCelular] = useState(
    estado ? props.location.state.celular : ""
  );

  const [email, setEmail] = useState(estado ? props.location.state.email : "");

  const history = useHistory();

  async function handleSalvar(e) {
    e.preventDefault();

    const dados = {
      nome,
      telefone,
      celular,
      email,
    };

    try {
      if (contatoId) {
        await api.put(`contatos/${contatoId}`, dados);

        alert("Contato atualizado com sucesso!");
      } else {
        const resposta = await api.post("contatos", dados);

        alert(`Seu ID de acesso: ${resposta.data.id}`);
      }

      history.push("/");
    } catch (erro) {
      const erroTelefone = erro.response.data.errors.Telefone
        ? erro.response.data.errors.Telefone[0]
        : "";

      const erroCelular = erro.response.data.errors.Celular
        ? erro.response.data.errors.Celular[0]
        : "";

      const erroEmail = erro.response.data.errors.Email
        ? erro.response.data.errors.Email[0]
        : "";

      alert(`${erroTelefone} ${erroCelular} ${erroEmail}`);
      history.push("/");
    }
  }

  return (
    <div className="salvar-container">
      <div className="content">
        <body>
          <Link className="link" to="/">
            <FiArrowLeft size={16} color="#483d8b" />
            Retornar
          </Link>

          <form onSubmit={handleSalvar}>
            <input
              placeholder="Nome"
              value={nome}
              onChange={(e) => setNome(e.target.value)}
            />
            <input
              placeholder="Telefone"
              value={telefone}
              onChange={(e) => setTelefone(e.target.value)}
            />
            <input
              placeholder="Celular"
              value={celular}
              onChange={(e) => setCelular(e.target.value)}
            />
            <input
              placeholder="E-mail"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
            />
            <button className="button" type="submit">
              Salvar
            </button>
          </form>
        </body>
      </div>
    </div>
  );
}
