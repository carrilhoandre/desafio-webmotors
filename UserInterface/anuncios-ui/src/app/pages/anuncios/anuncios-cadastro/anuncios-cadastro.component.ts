import { Component, OnInit, Injector } from '@angular/core';
import { BaseFormComponent } from 'src/app/core/components/base-form/base-form.component';
import { Anuncio } from 'src/app/core/models/anuncios/anuncio.model';
import { AnuncioService } from 'src/app/core/services/anuncios/anuncio.service';
import { Validators } from '@angular/forms';
import { MarcaService } from 'src/app/core/services/veiculos/marca.service';
import { ModeloService } from 'src/app/core/services/veiculos/modelo.service';
import { VersaoService } from 'src/app/core/services/veiculos/versao.service';
import { switchMap, mergeMap } from 'rxjs/operators';

@Component({
  selector: 'app-anuncios-cadastro',
  templateUrl: './anuncios-cadastro.component.html',
  styleUrls: ['./anuncios-cadastro.component.scss'],
})
export class AnunciosCadastroComponent extends BaseFormComponent<Anuncio> {
  validNameType: boolean = false;
  marcas: any;
  modelos: any;
  versoes: any;
  habilitaModelos: boolean = false;
  habilitaVersoes: boolean = false;

  constructor(
    protected _anuncioService: AnuncioService,
    private _marcaService: MarcaService,
    private _modeloService: ModeloService,
    private _versaoService: VersaoService,
    protected injector: Injector
  ) {
    super(injector, new Anuncio(), _anuncioService, Anuncio.fromJson);
    this.isLoading = true;
    this.listUrl = '/anuncios';
    this.marcas = [];
    this.modelos = [];
    this.versoes = [];
  }

  async ngOnInit() {
    this.setCurrentAction();
    this.buildResourceForm();
    this.obterMarcas();
  }

  obterMarcas() {
    this._marcaService.obterMarcas().subscribe((data) => {
      this.marcas = data;
      this.loadResource();
    });
  }

  protected bindResource() {
    this.resource.id = this.resourceForm.get('id').value;
    this.resource.marca = this.resourceForm.get('marca').value.Name;
    this.resource.modelo = this.resourceForm.get('modelo').value.Name;
    this.resource.versao = this.resourceForm.get('versao').value.Name;
    this.resource.ano = parseInt(this.resourceForm.get('ano').value);
    this.resource.quilometragem = parseInt(
      this.resourceForm.get('quilometragem').value
    );
    this.resource.observacao = this.resourceForm.get('observacao').value;
  }

  submitForm() {
    super.submitForm();
  }

  protected buildResourceForm() {
    this.resourceForm = this.formBuilder.group({
      id: [0],
      marca: [{}, [Validators.required]],
      modelo: [{}, [Validators.required]],
      versao: [{}, [Validators.required]],
      ano: ['', [Validators.required]],
      quilometragem: ['', [Validators.required]],
      observacao: ['', [Validators.required]],
    });
  }

  protected creationPageTitle(): string {
    return 'Cadastro do anuncio';
  }

  protected editionPageTitle(): string {
    return 'Alteração do anuncio';
  }

  onChangeMarca() {
    if (!this.firstLoad) {
      this.limpaOpcoes();
      if (this.resourceForm.get('marca').value != {}) {
        this._modeloService
          .obterModelosPorMarca(this.resourceForm.get('marca').value.ID)
          .subscribe((data) => {
            this.modelos = data;
            this.habilitaModelos = true;
          });
      } else this.modelos = [];
    }
  }

  onChangeModelo() {
    if (!this.firstLoad) {
      this.limpaVersoes();
      if (this.resourceForm.get('modelo').value != {}) {
        this._versaoService
          .obterVersoesPorModelo(this.resourceForm.get('modelo').value.ID)
          .subscribe((data) => {
            this.versoes = data;
            this.habilitaVersoes = true;
          });
      } else this.modelos = [];
    }
  }

  limpaOpcoes() {
    this.resourceForm.get('modelo').setValue({});
    this.resourceForm.get('versao').setValue({});
    this.habilitaModelos = false;
    this.habilitaVersoes = false;
  }

  limpaVersoes() {
    this.resourceForm.get('versao').setValue({});
  }

  protected loadResource() {
    if (this.currentAction == 'alterar') {
      this.resourceForm
        .get('id')
        .setValue(this.route.snapshot.paramMap.get('id'));
      this.route.paramMap
        .pipe(
          switchMap((params) => this.resourceService.getById(+params.get('id')))
        )
        .subscribe(
          (resource) => {
            this.resource = resource;
            this.resourceForm.patchValue(resource); // binds loaded resource data to resourceForm
            this.resourceForm
              .get('marca')
              .setValue(this.marcas.filter((c) => c.Name == resource.marca)[0]);
            this.loadSelects();
          },
          (error) => alert('Ocorreu um erro no servidor, tente mais tarde.')
        );
    } else {
      this.firstLoad = false;
      this.isLoading = false;
    }
  }

  loadSelects() {
    this._modeloService
      .obterModelosPorMarca(this.resourceForm.get('marca').value.ID)
      .pipe(
        mergeMap((modelos) => {
          this.modelos = modelos;
          var modelo = this.modelos.filter(
            (c) => c.Name == this.resource.modelo
          )[0];
          this.habilitaModelos = true;
          this.resourceForm.get('modelo').setValue(modelo);
          return this._versaoService.obterVersoesPorModelo(modelo.ID);
        })
      )
      .subscribe((v) => {
        this.versoes = v;
        var versao = this.versoes.filter(
          (c) => c.Name == this.resource.versao
        )[0];
        this.resourceForm.get('versao').setValue(versao);
        this.habilitaVersoes = true;
        this.isLoading = false;
        this.firstLoad = false;
      });
  }
}
