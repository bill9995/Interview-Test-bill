import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { UserService, UserDetailDto } from '../../../services/user.service';

@Component({
    standalone: true,
    selector: 'app-user-detail',
    imports: [CommonModule, RouterModule],
    templateUrl: './user-detail.component.html',
})
export class UserDetailComponent implements OnInit {
  user: UserDetailDto | null = null;
  loading: boolean = true;
  error: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private userService: UserService
  ) {}

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.loadUser(id);
      }
    });
  }

  loadUser(id: string) {
    this.userService.getUserById(id).subscribe({
      next: (data) => {
        this.user = data;
        this.loading = false;
      },
      error: (err: any) => {
        console.error('Error loading user details', err);
        this.error = 'Failed to load user details.';
        this.loading = false;
      }
    });
  }
}
