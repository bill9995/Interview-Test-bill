import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { UserService, UserListDto } from '../../../services/user.service';

@Component({
  standalone: true,
  selector: 'app-users-list',
  imports: [CommonModule, FormsModule, RouterModule],
    templateUrl: './users-list.component.html',
})
export class UsersListComponent implements OnInit {
  users: UserListDto[] = [];
  filteredUsers: UserListDto[] = [];
  loading: boolean = true;
  globalFilterValue: string = '';

  constructor(private userService: UserService) {}

  ngOnInit() {
    this.userService.getUsers().subscribe({
      next: (data) => {
        this.users = data;
        this.filteredUsers = data;
        this.loading = false;
      },
      error: (err: any) => {
        console.error('Error loading users', err);
        this.loading = false;
      }
    });
  }

  filterUsers() {
    if (!this.globalFilterValue) {
      this.filteredUsers = this.users;
      return;
    }
    const searchTerm = this.globalFilterValue.toLowerCase();
    this.filteredUsers = this.users.filter(u => 
      (u.id && u.id.toLowerCase().includes(searchTerm)) ||
      (u.userId && u.userId.toLowerCase().includes(searchTerm)) ||
      (u.username && u.username.toLowerCase().includes(searchTerm)) ||
      (u.name && u.name.toLowerCase().includes(searchTerm))
    );
  }
}
