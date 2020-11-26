import { Component, OnInit } from '@angular/core';
import { stringToKeyValue } from '@angular/flex-layout/extended/typings/style/style-transforms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MessageComponent, NewMessageData } from './message/message.component';
import { MessagehistoryComponent, MessageHistoryData } from './messagehistory/messagehistory.component';
import { Message, MessagesService } from '../services/messages.service';
import { PlayersService } from '../services/players.service';

@Component({
  selector: 'app-inbox',
  templateUrl: './inbox.component.html',
  styleUrls: ['./inbox.component.css']
})
export class InboxComponent implements OnInit {

  playerNames: { [id: number]: string };
  messages: Message[];
  messagesCount: number;
  pageSize: number = 10;
  pageSizeOptions: number[] = [5,10,25];
  pageIndex: number;
  sentBoxMode: boolean;

  constructor(private messageService: MessagesService, private playerService: PlayersService
    , public dialog: MatDialog) { }

  ngOnInit(): void {
    this.messageService.getMessageCount("").subscribe(c => this.messagesCount = c);
    this.messageService.getMessages(this.pageSize, 0).subscribe(m => this.messages = m);
    this.playerService.getCampaignPlayers().subscribe(p => 
      {
        this.playerNames = {};
        p.forEach(i => this.playerNames[i.id] = i.name);
      });
  }

  page(event: PageEvent) {
    if (this.sentBoxMode) {
      this.messageService.getSentMessages(event.pageSize, event.pageIndex).subscribe(m => this.messages = m);
    } else {
      this.messageService.getMessages(event.pageSize, event.pageIndex).subscribe(m => this.messages = m);
    }
  }

  setRead(message: Message): void {
    if (!message.isRead && !this.sentBoxMode)
    {
      message.isRead = true;
      this.messageService.updateMessage(message).subscribe();
    }
  }

  archive(message: Message): void {
    message.isArchived = true;
    this.messageService.updateMessage(message).subscribe();
  }

  remove(message: Message): void {
    var index: number = this.messages.indexOf(message);
    this.messages.splice(index, 1);
    this.messageService.deleteMessage(message.id).subscribe();
  }  

  newMessageDialog(): void {
    var messageData: NewMessageData = {
      isReply: false,
      recipient: null,
      subject: "",
      previousMessageId: null
    }
    this.dialog.open(MessageComponent, { data: messageData });
  }

  replyTo(message: Message): void {
    var messageData: NewMessageData = {
      isReply: true,
      recipient: message.senderId.toString(),
      subject: message.subject,
      previousMessageId: message.id
    }
    this.dialog.open(MessageComponent, { data: messageData });
  }

  history(message: Message): void {
    var historyData: MessageHistoryData = {
      message: message,
      playerNames: this.playerNames
    }
    this.dialog.open(MessagehistoryComponent, { data: historyData });
  }

  switchMode(): void {
    this.sentBoxMode = !this.sentBoxMode;
    if (this.sentBoxMode) {
      this.messageService.getMessageCount("sent").subscribe(c => this.messagesCount = c);
      this.messageService.getSentMessages(this.pageSize, 0).subscribe(m => this.messages = m);
    } else {
      this.messageService.getMessageCount("").subscribe(c => this.messagesCount = c);
      this.messageService.getMessages(this.pageSize, 0).subscribe(m => this.messages = m);
    }
  }
}
