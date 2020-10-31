import { Component, OnInit } from '@angular/core';
import { Message, MessagesService } from '../services/messages.service';
import { Player, PlayersService } from '../services/players.service';

@Component({
  selector: 'app-inbox',
  templateUrl: './inbox.component.html',
  styleUrls: ['./inbox.component.css']
})
export class InboxComponent implements OnInit {

  playerNames: { [id: number]: string };
  messages: Message[];

  constructor(private messageService: MessagesService, private playerService: PlayersService) { }

  ngOnInit(): void {
    this.messageService.getMessages().subscribe(m => this.messages = m);
    this.playerService.getCampaignPlayers().subscribe(p => 
      {
        this.playerNames = {};
        p.forEach(i => this.playerNames[i.id] = i.name);
      });
  }

  setRead(message: Message): void {
    if (!message.isRead)
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
}
