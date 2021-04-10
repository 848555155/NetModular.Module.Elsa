<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="VersionId：" prop="versionId">
          <el-input v-model="form.model.versionId" clearable />
        </el-form-item>
        <el-form-item label="DefinitionId：" prop="definitionId">
          <el-input v-model="form.model.definitionId" clearable />
        </el-form-item>
        <el-form-item label="Version：" prop="version">
          <el-input v-model="form.model.version" clearable />
        </el-form-item>
        <el-form-item label="Name：" prop="name">
          <el-input v-model="form.model.name" clearable />
        </el-form-item>
        <el-form-item label="Description：" prop="description">
          <el-input v-model="form.model.description" clearable />
        </el-form-item>
        <el-form-item label="Variables：" prop="variables">
          <el-input v-model="form.model.variables" clearable />
        </el-form-item>
        <el-form-item label="IsSingleton：" prop="isSingleton">
          <el-input v-model="form.model.isSingleton" clearable />
        </el-form-item>
        <el-form-item label="IsDisabled：" prop="isDisabled">
          <el-input v-model="form.model.isDisabled" clearable />
        </el-form-item>
        <el-form-item label="IsPublished：" prop="isPublished">
          <el-input v-model="form.model.isPublished" clearable />
        </el-form-item>
        <el-form-item label="IsLatest：" prop="isLatest">
          <el-input v-model="form.model.isLatest" clearable />
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-dialog>
</template>
<script>
import { mixins } from 'netmodular-ui'

const { add, edit, update } = $api.elsa.workflowDefinitionVersion

export default {
  mixins: [mixins.formSave],
  data() {
    return {
      title: 'WorkflowDefinitionVersion',
      actions: { add, edit, update },
      form: {
        model: {
          /** VersionId */
          versionId: '',
          /** DefinitionId */
          definitionId: '',
          /** Version */
          version: '',
          /** Name */
          name: '',
          /** Description */
          description: '',
          /** Variables */
          variables: '',
          /** IsSingleton */
          isSingleton: '',
          /** IsDisabled */
          isDisabled: '',
          /** IsPublished */
          isPublished: '',
          /** IsLatest */
          isLatest: ''
        },
        rules: {}
      }
    }
  }
}
</script>
