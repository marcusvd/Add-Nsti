import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';

import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'btn-g',
  templateUrl: './btn-g.component.html',
  styleUrls: ['./btn-g.component.scss'],
  standalone: true,
  imports: [CommonModule, MatButtonModule, MatIconModule]
})

export class BtnGComponent {

  @Output() btn = new EventEmitter<void>();
  @Input() name: string = '';
  @Input() haveIcon: boolean = true;
  @Input() icon: string = 'add';
  @Input() btnType: string = 'green'; //green, red, onlyIconGreen
  @Input() isDisabled: boolean = false;

  btnGMtd() {
    this.btn.emit();
  }
}
