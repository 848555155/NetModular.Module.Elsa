<template>
  <nm-container>
    <nm-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="VersionId：" prop="versionId">
          <el-input v-model="list.model.versionId" clearable />
        </el-form-item>
        <el-form-item label="DefinitionId：" prop="definitionId">
          <el-input v-model="list.model.definitionId" clearable />
        </el-form-item>
        <el-form-item label="Version：" prop="version">
          <el-input v-model="list.model.version" clearable />
        </el-form-item>
        <el-form-item label="Name：" prop="name">
          <el-input v-model="list.model.name" clearable />
        </el-form-item>
        <el-form-item label="Description：" prop="description">
          <el-input v-model="list.model.description" clearable />
        </el-form-item>
        <el-form-item label="Variables：" prop="variables">
          <el-input v-model="list.model.variables" clearable />
        </el-form-item>
        <el-form-item label="IsSingleton：" prop="isSingleton">
          <el-input v-model="list.model.isSingleton" clearable />
        </el-form-item>
        <el-form-item label="IsDisabled：" prop="isDisabled">
          <el-input v-model="list.model.isDisabled" clearable />
        </el-form-item>
        <el-form-item label="IsPublished：" prop="isPublished">
          <el-input v-model="list.model.isPublished" clearable />
        </el-form-item>
        <el-form-item label="IsLatest：" prop="isLatest">
          <el-input v-model="list.model.isLatest" clearable />
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

const api = $api.elsa.workflowDefinitionVersion

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
        }
      },
      removeAction: api.remove,
      buttons: page.buttons
    }
  }
}
</script>
