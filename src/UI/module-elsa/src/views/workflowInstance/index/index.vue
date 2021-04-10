<template>
  <nm-container>
    <nm-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="InstanceId：" prop="instanceId">
          <el-input v-model="list.model.instanceId" clearable />
        </el-form-item>
        <el-form-item label="DefinitionId：" prop="definitionId">
          <el-input v-model="list.model.definitionId" clearable />
        </el-form-item>
        <el-form-item label="Version：" prop="version">
          <el-input v-model="list.model.version" clearable />
        </el-form-item>
        <el-form-item label="Status：" prop="status">
          <el-input v-model="list.model.status" clearable />
        </el-form-item>
        <el-form-item label="CorrelationId：" prop="correlationId">
          <el-input v-model="list.model.correlationId" clearable />
        </el-form-item>
        <el-form-item label="CreatedAt：" prop="createdAt">
          <el-input v-model="list.model.createdAt" clearable />
        </el-form-item>
        <el-form-item label="StartedAt：" prop="startedAt">
          <el-input v-model="list.model.startedAt" clearable />
        </el-form-item>
        <el-form-item label="FinishedAt：" prop="finishedAt">
          <el-input v-model="list.model.finishedAt" clearable />
        </el-form-item>
        <el-form-item label="FaultedAt：" prop="faultedAt">
          <el-input v-model="list.model.faultedAt" clearable />
        </el-form-item>
        <el-form-item label="AbortedAt：" prop="abortedAt">
          <el-input v-model="list.model.abortedAt" clearable />
        </el-form-item>
        <el-form-item label="Scope：" prop="scope">
          <el-input v-model="list.model.scope" clearable />
        </el-form-item>
        <el-form-item label="Input：" prop="input">
          <el-input v-model="list.model.input" clearable />
        </el-form-item>
        <el-form-item label="ExecutionLog：" prop="executionLog">
          <el-input v-model="list.model.executionLog" clearable />
        </el-form-item>
        <el-form-item label="Fault：" prop="fault">
          <el-input v-model="list.model.fault" clearable />
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

const api = $api.elsa.workflowInstance

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
          /** InstanceId */
          instanceId: '',
          /** DefinitionId */
          definitionId: '',
          /** Version */
          version: '',
          /** Status */
          status: '',
          /** CorrelationId */
          correlationId: '',
          /** CreatedAt */
          createdAt: '',
          /** StartedAt */
          startedAt: '',
          /** FinishedAt */
          finishedAt: '',
          /** FaultedAt */
          faultedAt: '',
          /** AbortedAt */
          abortedAt: '',
          /** Scope */
          scope: '',
          /** Input */
          input: '',
          /** ExecutionLog */
          executionLog: '',
          /** Fault */
          fault: ''
        }
      },
      removeAction: api.remove,
      buttons: page.buttons
    }
  }
}
</script>
