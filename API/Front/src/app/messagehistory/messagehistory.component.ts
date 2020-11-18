import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Message, MessagesService } from '../services/messages.service';

export interface MessageHistoryData {
  message: Message;
  playerNames: { [id: number]: string };
}
@Component({
  selector: 'app-messagehistory',
  templateUrl: './messagehistory.component.html',
  styleUrls: ['./messagehistory.component.css']
})
export class MessagehistoryComponent implements OnInit {

  playerNames: { [id: number]: string };
  messagesList: Message[] = [];
  firstMessage: Message;

  constructor(public dialogRef: MatDialogRef<MessagehistoryComponent>, @Inject(MAT_DIALOG_DATA) public data: MessageHistoryData
      , private messagesService: MessagesService) { 
        this.firstMessage = data.message;
        this.playerNames = data.playerNames;
      }

  ngOnInit(): void {
    this.populateHistory(this.firstMessage);
  }

  populateHistory(message: Message): void {
    this.messagesList.push(message);
    if (message.previousMessageId) {
      this.messagesService.getMessage(message.previousMessageId).subscribe(m => this.populateHistory(m));
    }
    console.log(this.messagesList);
  }

}
