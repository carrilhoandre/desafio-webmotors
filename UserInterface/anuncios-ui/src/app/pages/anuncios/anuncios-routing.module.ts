import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AnunciosListaComponent } from './anuncios-lista/anuncios-lista.component';
import { AnunciosCadastroComponent } from './anuncios-cadastro/anuncios-cadastro.component';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: '',
        component: AnunciosListaComponent,
      },
      { path: 'novo', component: AnunciosCadastroComponent },
      { path: ':id/alterar', component: AnunciosCadastroComponent }
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AnunciosRoutingModule {}
