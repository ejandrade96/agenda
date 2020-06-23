import React, { useState, useEffect } from "react";
import { Link, useHistory } from "react-router-dom";
import { FiTrash2, FiEdit } from "react-icons/fi";

import "./styles.css";

import api from "../../services/api";

export default function Home() {
  const [contatos, setContatos] = useState([]);

  const history = useHistory();

  useEffect(() => {
    api.get("contatos").then((response) => {
      setContatos(response.data);
    });
  }, []);

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

  async function handleEdit(id, objeto) {
    history.push(`/editar/${id}`, objeto);
  }

  return (
    <div className="home-container">
      <div className="content">
        <header>
          <h1>Contatos</h1>
        </header>

        <body>
          <Link className="link" to="/salvar">
            Criar Novo Contato
          </Link>

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
        </body>
      </div>
    </div>
  );
}
