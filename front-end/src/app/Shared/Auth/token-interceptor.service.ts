import { HttpHeaders, HttpInterceptorFn } from "@angular/common/http";
import { inject } from "@angular/core";
import { AuthService } from "./Auth.service";
import { exhaustMap } from "rxjs";


export const addInterceptorForToken: HttpInterceptorFn = (req, next) => {
    const authService = inject(AuthService);
    const token = authService.getToken();
    if (!token) return next(req);
    const newReq = req.clone({ headers: new HttpHeaders({ "Authorization": 'Bearer ' + token }) })
    return next(newReq);
}