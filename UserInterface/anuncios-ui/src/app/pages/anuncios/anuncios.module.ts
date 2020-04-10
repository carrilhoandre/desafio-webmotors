import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgxPaginationModule } from 'ngx-pagination';
import { AnunciosRoutingModule } from './anuncios-routing.module';
import { AnunciosListaComponent } from './anuncios-lista/anuncios-lista.component';
import { AnunciosCadastroComponent } from './anuncios-cadastro/anuncios-cadastro.component';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxMaskModule, IConfig } from 'ngx-mask'
import { LoadingComponent } from 'src/app/core/components/loading/loading.component';
export const options: Partial<IConfig> | (() => Partial<IConfig>) = null;


@NgModule({
  declarations: [AnunciosListaComponent, AnunciosCadastroComponent, LoadingComponent],
  imports: [
    CommonModule,
    AnunciosRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    NgxPaginationModule,
    NgxMaskModule.forRoot(options),
  ]
})
export class AnunciosModule { }
