import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '@auth0/auth0-angular';
import { CustomerDetailsComponent } from '@customers/customer-details/customer-details.component';
import { CustomerListComponent } from '@customers/customer-list/customer-list.component';

const routes: Routes = [
  { 
    path: '', 
    canActivateChild: [AuthGuard],
    children: [
      { 
        path: '', 
        component: CustomerListComponent 
      },
      { 
        path: ':id', 
        component: CustomerDetailsComponent 
      }
    ]
  },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CustomersRoutingModule { }
