import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';

/* COMPONENTS */
import { LoginComponent } from './components/login/login.component';

const AuthenticationRoutes: Routes = [
    {
        path: '',
        children: [{
            path: 'login',
            component: LoginComponent
        }]
    }];

@NgModule({
    imports: [RouterModule.forChild(AuthenticationRoutes)],
    exports: [RouterModule]
})

export class AuthenticationRoutingModule { }
