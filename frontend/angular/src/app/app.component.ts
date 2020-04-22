import { Component } from '@angular/core';
import * as moment from 'moment';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'app';
  schedules = [];

  constructor() {
    for (let i = 0; i < 24; i = i + 1) {
      for (let j = 0; j <= 55; j = j + 5) {
        this.schedules.push(`${moment({ hour: i, minute: j }).format('HH:mm')}-${moment({ hour: i, minute: j + 4 }).format('HH:mm')}`);
      }
    }
  }
}
