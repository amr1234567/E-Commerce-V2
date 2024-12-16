import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CookieManagerService {

  constructor() { }

  setCookie(name: string, value: string, days: number = 7): void {
    const date = new Date();
    date.setTime(date.getTime() + days * 24 * 60 * 60 * 1000); // Calculate expiration time
    const expires = `expires=${date.toUTCString()}`;
    document.cookie = `${name}=${value}; ${expires}; path=/`;
  }

  // Get a cookie by name
  getCookie(name: string): string | undefined {
    const nameEQ = `${name}=`;
    const cookies = document.cookie.split(';');
    for (let i = 0; i < cookies.length; i++) {
      let cookie = cookies[i].trim();
      if (cookie.startsWith(nameEQ)) {
        return cookie.substring(nameEQ.length, cookie.length);
      }
    }
    return undefined;
  }

  // Delete a cookie
  deleteCookie(name: string): void {
    // Set the cookie's expiration date to a past date to remove it
    document.cookie = `${name}=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/`;
  }
}
