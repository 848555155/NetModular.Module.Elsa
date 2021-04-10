<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="WorkflowInstance：" prop="workflowInstance">
          <el-input v-model="form.model.workflowInstance" clearable />
        </el-form-item>
        <el-form-item label="ActivityId：" prop="activityId">
          <el-input v-model="form.model.activityId" clearable />
        </el-form-item>
        <el-form-item label="ActivityType：" prop="activityType">
          <el-input v-model="form.model.activityType" clearable />
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-dialog>
</template>
<script>
import { mixins } from 'netmodular-ui'

const { add, edit, update } = $api.elsa.blockingActivity

export default {
  mixins: [mixins.formSave],
  data() {
    return {
      title: 'BlockingActivity',
      actions: { add, edit, update },
      form: {
        model: {
          /** WorkflowInstance */
          workflowInstance: '',
          /** ActivityId */
          activityId: '',
          /** ActivityType */
          activityType: ''
        },
        rules: {}
      }
    }
  }
}
</script>
