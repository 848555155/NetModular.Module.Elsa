<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="ActivityId：" prop="activityId">
          <el-input v-model="form.model.activityId" clearable />
        </el-form-item>
        <el-form-item label="WorkflowInstance：" prop="workflowInstance">
          <el-input v-model="form.model.workflowInstance" clearable />
        </el-form-item>
        <el-form-item label="Type：" prop="type">
          <el-input v-model="form.model.type" clearable />
        </el-form-item>
        <el-form-item label="State：" prop="state">
          <el-input v-model="form.model.state" clearable />
        </el-form-item>
        <el-form-item label="Output：" prop="output">
          <el-input v-model="form.model.output" clearable />
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-dialog>
</template>
<script>
import { mixins } from 'netmodular-ui'

const { add, edit, update } = $api.elsa.activityInstance

export default {
  mixins: [mixins.formSave],
  data() {
    return {
      title: 'ActivityInstance',
      actions: { add, edit, update },
      form: {
        model: {
          /** ActivityId */
          activityId: '',
          /** WorkflowInstance */
          workflowInstance: '',
          /** Type */
          type: '',
          /** State */
          state: '',
          /** Output */
          output: ''
        },
        rules: {}
      }
    }
  }
}
</script>
