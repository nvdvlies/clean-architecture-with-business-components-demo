import { NgModule } from '@angular/core';
import { Route, RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '@auth0/auth0-angular';
import { DefaultTemplateComponent } from '@layout/default-template/default-template.component';
import { FeatureFlag } from '@shared/enums/feature-flag.enum';
import { Role } from '@shared/enums/role.enum';
import { ApplicationSettingsResolver } from '@shared/resolvers/application-settings.resolver';
import { CurrentUserResolver } from '@shared/resolvers/current-user.resolver';
import { FeatureFlagResolver } from '@shared/resolvers/feature-flag.resolver';
import { RolesResolver } from '@shared/resolvers/roles.resolver';
import { UserPreferencesResolver } from '@shared/resolvers/user-preferences.resolver';

export type RouteData = {
  featureFlag?: FeatureFlag;
  roleNames?: Array<keyof typeof Role>;
};

export type AppRoute = Route & {
  data?: RouteData;
};

export declare type AppRoutes = AppRoute[];

const routes: AppRoutes = [
  {
    path: '',
    component: DefaultTemplateComponent,
    canActivate: [AuthGuard],
    resolve: {
      featureFlagsInitialized: FeatureFlagResolver,
      applicationSettingsInitialized: ApplicationSettingsResolver,
      currentUserInitialized: CurrentUserResolver,
      userPreferencesInitialized: UserPreferencesResolver,
      rolesInitialized: RolesResolver
    },
    children: [
      {
        path: '',
        redirectTo: 'customers',
        pathMatch: 'full'
      },
      {
        path: 'customers',
        loadChildren: () =>
          import('./features/customers/customers.module').then((m) => m.CustomersModule),
        canActivate: [AuthGuard]
      },
      {
        path: 'invoices',
        loadChildren: () =>
          import('./features/invoices/invoices.module').then((m) => m.InvoicesModule),
        canActivate: [AuthGuard]
      }
    ] as AppRoutes
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
