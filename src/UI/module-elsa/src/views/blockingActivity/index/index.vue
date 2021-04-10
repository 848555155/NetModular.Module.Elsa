<template>
  <nm-container>
    <nm-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="WorkflowInstance：" prop="workflowInstance">
          <el-input v-model="list.model.workflowInstance" clearable />
        </el-form-item>
        <el-form-item label="ActivityId：" prop="activityId">
          <el-input v-model="list.model.activityId" clearable />
        </el-form-item>
        <el-form-item label="ActivityType：" prop="activityType">
          <el-input v-model="list.model.activityType" clearable />
        </el-form-item>
      </template>

      <!--按钮-->
      <template v-slot:querybar-buttons>
        <nm-button v-bind="buttons.add" @click="add" />
      </template>

      <!--自定义列-->
      <!-- <template v-slot:col-name="{row}">
        <nm-button :text="row.name" type="text" />
      </template> -->

      <!--操作列-->
      <template v-slot:col-operation="{ row }">
        <nm-button v-bind="buttons.edit" @click="edit(row)" />
        <nm-button-delete v-bind="buttons.del" :id="row.id" :action="removeAction" @success="refresh" />
      </template>
    </nm-list>

    <save-page :id="curr.id" :visible.sync="dialog.save" @success="refresh" />
  </nm-container>
</template>
<script>
import { mixins } from 'netmodular-ui'
import page from './page'
import cols from './cols'
import SavePage from '../components/save'

const api = $api.elsa.blockingActivity

export default {
  name: page.name,
  mixins: [mixins.list],
  components: { SavePage },
  data() {
    return {
      list: {
        title: page.title,
        cols,
        action: api.query,
        model: {
          /** WorkflowInstance */
          workflowInstance: '',
          /** ActivityId */
          activityId: '',
          /** ActivityType */
          activityType: ''
        }
      },
      removeAction: api.remove,
      buttons: page.buttons
    }
  }
}
</script>
