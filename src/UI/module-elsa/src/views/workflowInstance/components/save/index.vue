<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="InstanceId：" prop="instanceId">
          <el-input v-model="form.model.instanceId" clearable />
        </el-form-item>
        <el-form-item label="DefinitionId：" prop="definitionId">
          <el-input v-model="form.model.definitionId" clearable />
        </el-form-item>
        <el-form-item label="Version：" prop="version">
          <el-input v-model="form.model.version" clearable />
        </el-form-item>
        <el-form-item label="Status：" prop="status">
          <el-input v-model="form.model.status" clearable />
        </el-form-item>
        <el-form-item label="CorrelationId：" prop="correlationId">
          <el-input v-model="form.model.correlationId" clearable />
        </el-form-item>
        <el-form-item label="CreatedAt：" prop="createdAt">
          <el-input v-model="form.model.createdAt" clearable />
        </el-form-item>
        <el-form-item label="StartedAt：" prop="startedAt">
          <el-input v-model="form.model.startedAt" clearable />
        </el-form-item>
        <el-form-item label="FinishedAt：" prop="finishedAt">
          <el-input v-model="form.model.finishedAt" clearable />
        </el-form-item>
        <el-form-item label="FaultedAt：" prop="faultedAt">
          <el-input v-model="form.model.faultedAt" clearable />
        </el-form-item>
        <el-form-item label="AbortedAt：" prop="abortedAt">
          <el-input v-model="form.model.abortedAt" clearable />
        </el-form-item>
        <el-form-item label="Scope：" prop="scope">
          <el-input v-model="form.model.scope" clearable />
        </el-form-item>
        <el-form-item label="Input：" prop="input">
          <el-input v-model="form.model.input" clearable />
        </el-form-item>
        <el-form-item label="ExecutionLog：" prop="executionLog">
          <el-input v-model="form.model.executionLog" clearable />
        </el-form-item>
        <el-form-item label="Fault：" prop="fault">
          <el-input v-model="form.model.fault" clearable />
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-dialog>
</template>
<script>
import { mixins } from 'netmodular-ui'

const { add, edit, update } = $api.elsa.workflowInstance

export default {
  mixins: [mixins.formSave],
  data() {
    return {
      title: 'WorkflowInstance',
      actions: { add, edit, update },
      form: {
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
        },
        rules: {}
      }
    }
  }
}
</script>
