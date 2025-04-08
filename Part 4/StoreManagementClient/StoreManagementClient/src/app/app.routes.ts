import { Routes } from '@angular/router';
import { SignupComponent } from './components/signup/signup.component';
import { PlaceOrderComponent } from './components/place-order/place-order.component';
import { ViewOrdersComponent } from './components/view-orders/view-orders.component';
import { SigninComponent } from './components/signin/signin.component';

export const routes: Routes = [{ path: '', component: SigninComponent, pathMatch: 'full' },
    { path: 'Signin', component: SigninComponent, pathMatch: 'full' },
    { path: 'Signup', component: SignupComponent, pathMatch: 'full' },
    { path: 'Order', component: PlaceOrderComponent, pathMatch: 'full' },
    { path: 'View', component: ViewOrdersComponent, pathMatch: 'full' }
];
