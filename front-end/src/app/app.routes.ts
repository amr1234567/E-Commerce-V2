import { CanActivateFn, RedirectCommand, Router, Routes, UrlTree } from '@angular/router';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { canActive } from './Shared/Auth/canActive.Gard';

export const routes: Routes = [
    {
        path: '',
        component: AppComponent,
        canActivate: [canActive],
        children: [
            {
                path: '',
                loadChildren: () => import("./contacts-page/contacts-page.component").then(module => module.ContactsPageComponent)
            },
            {
                path: 'contact/:id',
                loadChildren: () => import("./contact-page/contact-page.component").then(module => module.ContactPageComponent)
            }
        ]
    },
    {
        path: "/login",
        component: LoginComponent
    },
    {
        path: '**',
        redirectTo: ''
    }
];



