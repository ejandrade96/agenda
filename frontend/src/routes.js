import React from "react";
import { BrowserRouter, Route, Switch } from "react-router-dom";

import Home from "./pages/Home";
import Salvar from "./pages/Salvar";
import NotFound from "./pages/NotFound";

export default function Routes() {
  return (
    <BrowserRouter>
      <Switch>
        <Route path="/" exact component={Home} />
        <Route path="/salvar" component={Salvar} />
        <Route path="/editar/:contatoId" component={Salvar} />
        <Route component={NotFound} />
      </Switch>
    </BrowserRouter>
  );
}
