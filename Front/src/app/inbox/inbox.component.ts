import { Component, OnInit } from '@angular/core';

export interface Message {
  title: string;
  sender: string;
  body: string;
  isRead: boolean;
  isArchived: boolean;
  canAnswer: boolean;
}
@Component({
  selector: 'app-inbox',
  templateUrl: './inbox.component.html',
  styleUrls: ['./inbox.component.css']
})
export class InboxComponent implements OnInit {

  messages: Message[] =
  [
    { title: 'Quelqu\'un vous calomnie', sender: 'Médisance', body: 'Vous gagnez 3 points d\'Infamie', isRead: false, isArchived: false, canAnswer:false },
    { title: 'Accord de flatterie ?', sender: 'Hida Kataki', body: 'Salut poto ! Ca te dirait je te fais une flatterie, tu me fais une flatterie ?', isRead: false, isArchived: false, canAnswer:true },
    { title: 'Victoire !', sender: 'Bataille', body: 'Votre armée a affronté une armée de Doji Misao en C2, et a remporté la victoire !', isRead: true, isArchived: true, canAnswer:false }
  ];

  constructor() { }

  ngOnInit(): void {
  }

  archive(message: Message): void {
    message.isArchived = true;
  }

  remove(message: Message): void {
    var index: number = this.messages.indexOf(message);
    this.messages.splice(index, 1);
  }
  
}
