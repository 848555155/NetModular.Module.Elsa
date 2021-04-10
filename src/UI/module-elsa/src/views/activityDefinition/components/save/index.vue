<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="ActivityId：" prop="activityId">
          <el-input v-model="form.model.activityId" clearable />
        </el-form-item>
        <el-form-item label="WorkflowDefinitionVersion：" prop="workflowDefinitionVersion">
          <el-input v-model="form.model.workflowDefinitionVersion" clearable />
        </el-form-item>
        <el-form-item label="Type：" prop="type">
          <el-input v-model="form.model.type" clearable />
        </el-form-item>
        <el-form-item label="Name：" prop="name">
          <el-input v-model="form.model.name" clearable />
        </el-form-item>
        <el-form-item label="DisplayName：" prop="displayName">
          <el-input v-model="form.model.displayName" clearable />
        </el-form-item>
        <el-form-item label="Description：" prop="description">
          <el-input v-model="form.model.description" clearable />
        </el-form-item>
        <el-form-item label="Left：" prop="left">
          <el-input v-model="form.model.left" clearable />
        </el-form-item>
        <el-form-item label="Top：" prop="top">
          <el-input v-model="form.model.top" clearable />
        </el-form-item>
        <el-form-item label="State：" prop="state">
          <el-input v-model="form.model.state" clearable />
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-dialog>
</template>
<script>
import { mixins } from 'netmodular-ui'

const { add, edit, update } = $api.elsa.activityDefinition

export default {
  mixins: [mixins.formSave],
  data() {
    return {
      title: 'ActivityDefinition',
      actions: { add, edit, update },
      form: {
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
        },
        rules: {}
      }
    }
  }
}
</script>
