import { Component, OnInit } from '@angular/core';
import { Router, RouterModule, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})

export class AppComponent implements OnInit {

  constructor(private _router: Router) { }

  ngOnInit(): void {
    // this._router.navigateByUrl('/');
    this._router.navigateByUrl('/customers/list');
  }

  title = 'im';
}
