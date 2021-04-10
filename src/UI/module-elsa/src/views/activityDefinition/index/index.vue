<template>
  <nm-container>
    <nm-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="ActivityId：" prop="activityId">
          <el-input v-model="list.model.activityId" clearable />
        </el-form-item>
        <el-form-item label="WorkflowDefinitionVersion：" prop="workflowDefinitionVersion">
          <el-input v-model="list.model.workflowDefinitionVersion" clearable />
        </el-form-item>
        <el-form-item label="Type：" prop="type">
          <el-input v-model="list.model.type" clearable />
        </el-form-item>
        <el-form-item label="Name：" prop="name">
          <el-input v-model="list.model.name" clearable />
        </el-form-item>
        <el-form-item label="DisplayName：" prop="displayName">
          <el-input v-model="list.model.displayName" clearable />
        </el-form-item>
        <el-form-item label="Description：" prop="description">
          <el-input v-model="list.model.description" clearable />
        </el-form-item>
        <el-form-item label="Left：" prop="left">
          <el-input v-model="list.model.left" clearable />
        </el-form-item>
        <el-form-item label="Top：" prop="top">
          <el-input v-model="list.model.top" clearable />
        </el-form-item>
        <el-form-item label="State：" prop="state">
          <el-input v-model="list.model.state" clearable />
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

const api = $api.elsa.activityDefinition

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
          /** ActivityId */
          activityId: '',
          /** WorkflowDefinitionVersion */
          workflowDefinitionVersion: '',
          /** Type */
          type: '',
          /** Name */
          name: '',
          /** DisplayName */
          displayName: '',
          /** Description */
          description: '',
          /** Left */
          left: '',
          /** Top */
          top: '',
          /** State */
          state: ''
        }
      },
      removeAction: api.remove,
      buttons: page.buttons
    }
  }
}
</script>
