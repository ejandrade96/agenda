/* eslint-disable no-undef */
describe("Testes para tela de login", () => {
  it("Deve validar se a página contém um título igual a 'Faça seu login'", () => {
    cy.visit("/login");

    cy.contains("Faça seu login").should("to.have.length", 1);
  });

  it("Deve validar se a página contém dois inputs", () => {
    cy.visit("/login");

    cy.get("form").find("input").should("to.have.length", 2);
  });

  it("Deve validar se a página contém um botão", () => {
    cy.visit("/login");

    cy.get("form").find("button").should("to.have.length", 1);
  });

  it("Deve validar se a página contém uma imagem", () => {
    cy.visit("/login");

    cy.get("[data-cy=login-container]").find("img").should("to.have.length", 1);
  });

  it("Deve validar se a página contém um link", () => {
    cy.visit("/login");

    cy.get("[data-cy=login-container]").find("a").should("to.have.length", 1);
  });

  it("Deve direcionar para tela de registro quando clicar no link não tenho cadastro", () => {
    cy.visit("/login");

    cy.get("[data-cy=link-nao-tenho-cadastro]").click();

    cy.url().should("include", "/registro");
  });
});
