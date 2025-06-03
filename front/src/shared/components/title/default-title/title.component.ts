import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';


@Component({
  selector: 'title-component',
  templateUrl: './title.component.html',
  styleUrl: './title.component.scss',
  standalone: true,
  imports: [CommonModule, MatIconModule, MatButtonModule]
})
export class TitleComponent {

  @Input() digit!: string;
  @Input() textTitleComp!: string;
  @Input() icon!: string;

  back() {
    window.history.back();
  }

}
