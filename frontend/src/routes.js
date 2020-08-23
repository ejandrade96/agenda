import React from "react";
import { BrowserRouter, Route, Switch, Redirect } from "react-router-dom";

import Login from "./pages/Login";
import Registro from "./pages/Registro";
import Home from "./pages/Home";
import SalvarContato from "./pages/SalvarContato";
import NotFound from "./pages/NotFound";

export default function Routes() {
  return (
    <BrowserRouter>
      <Switch>
        <Route path="/" exact>
          <Redirect to="/login" />
        </Route>
        <Route path="/login" component={Login} />
        <Route path="/registro" component={Registro} />
        <Route path="/home" component={Home} />
        <Route path="/salvar/contato" component={SalvarContato} />
        <Route path="/editar/:contatoId" component={SalvarContato} />
        <Route component={NotFound} />
      </Switch>
    </BrowserRouter>
  );
}
