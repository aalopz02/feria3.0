import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'home',
    loadChildren: () => import('./home/home.module').then( m => m.HomePageModule)
  },
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  },
  {
    path: 'register',
    loadChildren: () => import('./client/register/register.module').then( m => m.RegisterPageModule)
  },
  {
    path: 'basic',
    loadChildren: () => import('./basic/basic.module').then( m => m.BasicPageModule)
  },
  {
    path: 'update',
    loadChildren: () => import('./client/update/update.module').then( m => m.UpdatePageModule)
  },
  {
    path: 'cart',
    loadChildren: () => import('./shop/cart/cart.module').then( m => m.CartPageModule)
  },
  {
    path: 'producers',
    loadChildren: () => import('./shop/producers/producers.module').then( m => m.ProducersPageModule)
  },
  {
    path: 'products',
    loadChildren: () => import('./shop/products/products.module').then( m => m.ProductsPageModule)
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
