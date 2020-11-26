import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Message, MessagesService } from '../../services/messages.service';
import { OptionsService } from '../../services/options.service';

export interface PreviewBlock {
  content: string;
  prefix: string;
  isSelected: boolean;
}
export interface NewMessageData {
  previousMessageId: number;
  isReply: boolean;
  recipient: string;
  subject: string;
}

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.css']
})
export class MessageComponent implements OnInit {
  constructor(public dialogRef: MatDialogRef<MessageComponent>, @Inject(MAT_DIALOG_DATA) public data: NewMessageData
      , private optionsService: OptionsService, private messagesService: MessagesService, private snackBar: MatSnackBar) {
        this.isReply = data.isReply;
        this.selectedRecipient = data.recipient;
        this.subject = data.subject;
        this.previousMessageId = data.previousMessageId;
      }

  subject: string = "";
  body: string = "";
  previewMessage: PreviewBlock[] = [];
  selectedRecipient: string;
  recipientOptions: { [id: string]: string; } = {};
  isReply: boolean;
  previousMessageId: number;
  isWriting: boolean;
  isFormatting: boolean;
  isSending: boolean;
  errorRecipientRequired: string;
  errorSubjectRequired: string;
  errorBodyRequired: string;

  ngOnInit(): void {
    this.optionsService.getOptions("Opponent", null).subscribe(l => {
      this.recipientOptions = l;
    });
    this.isWriting = true;
  }

  back(): void {
    this.isWriting = true;
    this.isFormatting = false;
    var firstBlock: boolean = true;
  }

  preview(): void {
    if (!this.selectedRecipient) {
      this.errorRecipientRequired = "Champ obligatoire";
    }
    if (this.subject.length < 1) {
      this.errorSubjectRequired = "Champ obligatoire";
    }
    if (this.body.length < 1) {
      this.errorSubjectRequired = "Champ obligatoire";
    }
    if (this.selectedRecipient && this.subject.length > 0 && this.body.length > 0) {
      this.previewMessage = [];
      this.body.split('\n').forEach(p => {
        var block: PreviewBlock = { content: p, prefix: "", isSelected: false };
        this.previewMessage.push(block);
      });
      this.isWriting = false;
      this.isFormatting = true;
    } else {
      this.snackBar.open("Champ obligatoire manquant", "", { duration : 2500 })
    }
  }

  format(prefix: string): void {
    this.previewMessage.forEach(b => {
      if (b.isSelected) {
        b.prefix = prefix;
        b.isSelected = false;
      }
    });
  }

  send(): void {
    this.isSending = true;
    this.body = "";
    var firstBlock: boolean = true;
    this.previewMessage.forEach(b => {
      if (!firstBlock)
      {
        this.body += '\r\n';
      }
      this.body += b.prefix + b.content;
      firstBlock = false;
    });
    var message: Message = { 
      subject: this.subject,
      body: this.body,
      playerId: +this.selectedRecipient,
      isNotification: false,
      isRead: false,
      isArchived: false,
      senderId: 0,
      id: 0,
      sendDate: null,
      previousMessageId: this.previousMessageId,
      turn: 0
    }
    this.messagesService.sendMessage(message).subscribe(
      res => {
        this.dialogRef.close();
        this.snackBar.open("Message envoyé", "", { duration : 2500 })
      },
      err => {
        this.isSending = false;
        this.snackBar.open("Erreur à l'envoi du message", "", { duration : 2500 })
        console.log('HTTP Error', err)
      });
  }
}
