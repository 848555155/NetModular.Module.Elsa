<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="WorkflowDefinitionVersion：" prop="workflowDefinitionVersion">
          <el-input v-model="form.model.workflowDefinitionVersion" clearable />
        </el-form-item>
        <el-form-item label="SourceActivityId：" prop="sourceActivityId">
          <el-input v-model="form.model.sourceActivityId" clearable />
        </el-form-item>
        <el-form-item label="DestinationActivityId：" prop="destinationActivityId">
          <el-input v-model="form.model.destinationActivityId" clearable />
        </el-form-item>
        <el-form-item label="Outcome：" prop="outcome">
          <el-input v-model="form.model.outcome" clearable />
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-dialog>
</template>
<script>
import { mixins } from 'netmodular-ui'

const { add, edit, update } = $api.elsa.connectionDefinition

export default {
  mixins: [mixins.formSave],
  data() {
    return {
      title: 'ConnectionDefinition',
      actions: { add, edit, update },
      form: {
        model: {
          /** WorkflowDefinitionVersion */
          workflowDefinitionVersion: '',
          /** SourceActivityId */
          sourceActivityId: '',
          /** DestinationActivityId */
          destinationActivityId: '',
          /** Outcome */
          outcome: ''
        },
        rules: {}
      }
    }
  }
}
</script>
