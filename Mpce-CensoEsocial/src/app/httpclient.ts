import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';

@Injectable({
  providedIn: 'root'
})

export class HttpClient {

  constructor(private http: Http) { }

  createAuthorizationHeader(headers: Headers, token: string) {
      headers.append('Content-Type', 'application/json');
      headers.append('Authorization', 'Bearer '
          + token);
  }

  get(url, token) {
      const headers = new Headers();
      this.createAuthorizationHeader(headers, token);
      return this.http.get(url, {
          headers: headers
      });
  }

  getWithOutHeader(url) {
      return this.http.get(url);
  }

  post(url, data, token) {
      let headers = new Headers();
      this.createAuthorizationHeader(headers, token);
      return this.http.post(url, data, {
          headers: headers
      });
  }

  put(url, data, token) {
      let headers = new Headers();
      this.createAuthorizationHeader(headers, token);
      return this.http.put(url, data, {
          headers: headers
      });
  }

  delete(url, token) {
      let headers = new Headers();
      this.createAuthorizationHeader(headers, token);
      return this.http.delete(url, {
          headers: headers
      });
  }

 
}
export class CadastroService {

  constructor() { }
}
