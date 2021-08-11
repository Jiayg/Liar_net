import { IAsyncRouteState } from '@/store/modules/async-route'
import { UserState } from '@/store/modules/user/state'
import { ILockscreenState } from '@/store/modules/lockscreen'
import { ITabsViewState } from '@/store/modules/tabs-view'

export interface IStore {
  asyncRoute: IAsyncRouteState
  user: UserState
  lockscreen: ILockscreenState
  tabsView: ITabsViewState
}
