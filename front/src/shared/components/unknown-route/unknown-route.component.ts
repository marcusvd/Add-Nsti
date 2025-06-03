import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-unknown-route',
  standalone: true,
  imports: [
    MatButtonModule,
    MatCardModule,
    MatIconModule,
    RouterModule
  ],
  templateUrl: './unknown-route.component.html',
  styleUrl: './unknown-route.component.scss'
})
export class UnknownRouteComponent {

}
