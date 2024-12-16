import { CanActivateFn, RedirectCommand, Router } from "@angular/router";
import { AuthService } from "./Auth.service";
import { inject } from "@angular/core";

export const canActive: CanActivateFn = (activatedRouteSnapshot, routerStateSnapshot) => {
    const authSerive = inject(AuthService);
    if (authSerive.userLoggedIn()) {
        return true;
    }
    const router = inject(Router)
    return new RedirectCommand(router.parseUrl("/login"));
}