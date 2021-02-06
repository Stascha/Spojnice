import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { GameTabViewComponent } from './components/game-tab-view/game-tab-view.component';
import { GameComponent } from './components/game/game.component';
import { IndexComponent } from './components/index/index.component';
import { LoginComponent } from './components/login/login.component';

const routes: Routes = [
  {path : '', component: IndexComponent ,pathMatch: 'full' },
  {path : 'login', component: LoginComponent},
  {path : 'game', component: GameComponent},
  {path : 'game-table-view', component: GameTabViewComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
