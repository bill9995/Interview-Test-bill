import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

export interface UserListDto {
  id: string;
  userId: string;
  username: string;
  name: string;
  age: number | null;
  rolesCount: number;
  permissionsCount: number;
}

export interface UserRoleDto {
  roleId: number;
  roleName: string;
}

export interface UserDetailDto {
  id: string;
  userId: string;
  username: string;
  firstName: string;
  lastName: string;
  age: number | null;
  roles: UserRoleDto[];
  permissions: string[];
}

@Injectable({ providedIn: 'root' })
export class UserService {
  private apiUrl = `${environment.gatewayApiUrl}/api/User`;

  constructor(private http: HttpClient) {}

  getUsers(): Observable<UserListDto[]> {
    const headers = new HttpHeaders({ 'x-api-key': environment.apiKey });
    return this.http.get<UserListDto[]>(`${this.apiUrl}/GetUsers`, { headers });
  }

  getUserById(id: string): Observable<UserDetailDto> {
    const headers = new HttpHeaders({ 'x-api-key': environment.apiKey });
    return this.http.get<UserDetailDto>(`${this.apiUrl}/GetUserById/${id}`, { headers });
  }
}
