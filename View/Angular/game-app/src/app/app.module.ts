import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { IndexComponent } from './components/index/index.component';
import { LoginComponent } from './components/login/login.component';
import { GameComponent } from './components/game/game.component';
import { FormsModule } from '@angular/forms';
import { UserService } from './services/user.service';
import { HttpClientModule } from '@angular/common/http';
import { ScoreService } from './services/score.service';
import { GameService } from './services/game.service';
import { ScoreTabComponent } from './components/game/score-tab/score-tab.component';
import { UserActionsBtnsComponent } from './components/game/user-actions-btns/user-actions-btns.component';
import { GameTabViewComponent } from './components/game-tab-view/game-tab-view.component';

@NgModule({
  declarations: [
    AppComponent,
    IndexComponent,
    LoginComponent,
    GameComponent,
    ScoreTabComponent,
    UserActionsBtnsComponent,
    GameTabViewComponent
  ],
  imports: [
    HttpClientModule, 
    BrowserModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [UserService, ScoreService, GameService, ScoreTabComponent, GameComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
