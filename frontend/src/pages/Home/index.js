import React, { useState, useEffect } from "react";
import { Link, useHistory } from "react-router-dom";
import { FiTrash2, FiEdit, FiPower } from "react-icons/fi";

import "./styles.css";

import api from "../../services/api";

export default function Home() {
  const usuarioNome = localStorage.getItem("usuarioNome");
  const usuarioId = localStorage.getItem("usuarioId");
  const [inputBusca, setInputBusca] = useState("");
  const [contatos, setContatos] = useState([]);

  const history = useHistory();

  useEffect(() => {
    api.get(`usuarios/${usuarioId}/contatos`).then((response) => {
      setContatos(response.data);
    });
  }, [usuarioId]);

  async function handleDelete(id) {
    try {
      await api.delete(`contatos/${id}`);

      setContatos(contatos.filter((contato) => contato.id !== id));
    } catch (erro) {
      alert(
        `${erro.response.data.mensagem} Status Code ${erro.response.status}.`
      );
    }
  }

  function handleSair() {
    localStorage.clear();

    history.push("/login");
  }

  const handleEdit = (id, objeto) => history.push(`/editar/${id}`, objeto);

  // const handleChange = (evento) => setInputBusca(evento.target.value);

  function handleChange(evento) {
    setContatos(
      contatos.filter((contato) =>
        contato.nome.toLowerCase().includes(evento.target.value.toLowerCase())
      )
    );

    // setInputBusca(evento.target.value);
  }

  return (
    <div className="home-container">
      <header>
        <span>
          Olá, <strong>{usuarioNome}</strong>!
        </span>
        <Link className="btn-top-nav" to="/salvar/contato">
          Cadastrar Novo Contato
        </Link>

        <button onClick={handleSair} className="btn-top-nav" type="button">
          <FiPower size={16} />
        </button>
      </header>

      <div className="content">
        <div>
          <form>
            <input placeholder="Procurar" onChange={handleChange} />
          </form>
        </div>

        <table className="table-contatos">
          <thead>
            <tr>
              <th></th>
              <th>Nome</th>
              <th>Telefone</th>
              <th>Celular</th>
              <th>Email</th>
              <th>Ações</th>
            </tr>
          </thead>

          <tbody>
            {contatos.map((contato) => (
              <tr key={contato.id}>
                <td></td>
                <td>{contato.nome}</td>
                <td>{contato.telefone}</td>
                <td>{contato.celular}</td>
                <td>{contato.email}</td>
                <td>
                  <button
                    type="button"
                    className="button-action"
                    onClick={() => handleEdit(contato.id, contato)}
                  >
                    <FiEdit />
                  </button>
                  <button
                    type="button"
                    className="button-action"
                    onClick={() => handleDelete(contato.id)}
                  >
                    <FiTrash2 />
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
