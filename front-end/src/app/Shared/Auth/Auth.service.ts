import { computed, inject, Injectable, signal } from '@angular/core';
import { User } from './User.model';
import { CookieManagerService } from '../CookieManager.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private cookieService = inject(CookieManagerService);
  private user = signal<User | null>(null);
  private tokenExpires = 10;
  constructor() { }


  setUser(user: User | null) {
    if (user) {
      if (!user.token)
        throw new Error("User's token is required");
      this.cookieService.setCookie(`${user?.id}|token`, user?.token, this.tokenExpires);
      const savedObject = {
        id: user?.id,
        email: user?.email,
        role: user?.role,
      };
      localStorage.setItem(`user`, JSON.stringify(savedObject));
    }
    this.user.set(user);
  }

  autoLogin() {
    const savedObjectAsString = localStorage.getItem(`user`);
    if (!savedObjectAsString)
      return;
    const savedObject = JSON.parse(savedObjectAsString);
    if (!savedObject)
      return;
    this.setUser({
      id: savedObject.id,
      email: savedObject.email,
      role: savedObject.role,
      token: this.cookieService.getCookie(`${savedObject.id}|token`),
    });
    if (!this.user()) {
      localStorage.removeItem(`user`);
      this.setUser(null);
      this.cookieService.deleteCookie(`${savedObject.id}|token`);
    }
  }

  getToken() {
    return computed(() => this.user()?.token);
  }

  get userLoggedIn() {
    return computed(() => !!this.user());
  }

  get IsUserAdmin() {
    return computed(() => this.user()?.role === 'admin');
  }
}

