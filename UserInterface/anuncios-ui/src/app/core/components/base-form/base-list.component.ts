import { OnInit } from '@angular/core';
import { ResultModel } from 'src/app/core/models/result.model';
import { BaseResourceModel } from '../../models/base.model';
import { BaseResourceService } from '../../services/base.resource.service';


export abstract class BaseListComponent<T extends BaseResourceModel> implements OnInit {
  public isLoading: boolean;
  resources: ResultModel<T> = {};
  protected searchCommand : any
  public p: number = 1;

  constructor(private resourceService: BaseResourceService<T>) { 
  }

  ngOnInit() {
    this.getPage(1);
  }

  pageChanged($event) {
    this.getPage($event);
  }

  getPage(page: number) {
    this.isLoading = true;
    this.p = page;
    this.searchCommand.page = this.p;
    this.resourceService.getAll(this.searchCommand).subscribe(
      resources => 
      {
        this.resources = resources;
        this.isLoading = false;
      },
      error => {
        this.isLoading = false;
        alert('Erro ao carregar a lista');
    }
    )
}

  deleteResource(resource: T) {
    const mustDelete = confirm('Deseja realmente excluir este item?');
    
    if (mustDelete){
      this.resourceService.delete(resource.id).subscribe(
        () => {this.getPage(1)},
        () => alert("Erro ao tentar excluir!")
      )
    }
  }

}