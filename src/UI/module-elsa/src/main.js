import WebHost from 'netmodular-module-admin'
import config from './config'
import Elsa from './index'

// 注入模块
WebHost.registerModule(Elsa)

// 启动
WebHost.start(config)
