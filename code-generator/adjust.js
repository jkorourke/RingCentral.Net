// Adjust swagger spec, because it is not 100% correct
import yaml from 'js-yaml'
import fs from 'fs'

const doc = yaml.safeLoad(fs.readFileSync('rc-platform.yml', 'utf8'))

// remove duplicate scim endpoints
delete doc.paths['/scim/health']
delete doc.paths['/scim/Users']
delete doc.paths['/scim/ServiceProviderConfig']

// Support multiple attachments: https://git.ringcentral.com/platform/api-metadata-specs/issues/21
const sendFaxParams = doc.paths['/restapi/v1.0/account/{accountId}/extension/{extensionId}/fax'].post.parameters
const faxAttachment = sendFaxParams.filter(p => p.name === 'attachment')[0]
faxAttachment.type = 'array'
faxAttachment.collectionFormat = 'multi'
faxAttachment.items = { type: 'file' }
faxAttachment.name = 'attachments'
const faxTo = sendFaxParams.filter(p => p.name === 'to')[0]
if (faxTo.items.type === 'string') {
  delete faxTo.items.type
  faxTo.items.$ref = '#/definitions/MessageStoreCalleeInfoRequest'
}

// https://jira.ringcentral.com/browse/PLD-337?filter=-2
// https://github.com/ringcentral/RingCentral.Net/issues/12
doc.definitions.MessageStoreCalleeInfoRequest = {
  type: 'object',
  properties: {
    phoneNumber: {
      type: 'string',
      description: 'Phone number in E.164 format'
    },
    name: {
      type: 'string',
      description: 'Name of the callee'
    }
  }
}

// https://git.ringcentral.com/platform/api-metadata-specs/issues/48
const p1 = doc.paths['/restapi/v1.0/account/{accountId}/greeting'].post
p1.parameters = p1.parameters.filter(p => p.name !== 'answeringRuleId')
p1.parameters.push({
  name: 'answeringRule',
  in: 'formData',
  $ref: '#/definitions/CustomCompanyGreetingAnsweringRuleInfo'
})
const p2 = doc.paths['/restapi/v1.0/account/{accountId}/extension/{extensionId}/greeting'].post
p2.parameters = p2.parameters.filter(p => p.name !== 'answeringRuleId')
p2.parameters.push({
  name: 'answeringRule',
  in: 'formData',
  $ref: '#/definitions/CustomGreetingAnsweringRuleInfoRequest'
})
doc.definitions.CustomCompanyGreetingAnsweringRuleInfo = doc.definitions.CustomGreetingAnsweringRuleInfoRequest = {
  type: 'object',
  properties: {
    id: {
      type: 'string'
    }
  }
}

// https://jira.ringcentral.com/browse/PLD-592
// doc.definitions.PlayTarget.properties.resources = doc.definitions.PlayResources
doc.paths['/restapi/v1.0/account/{accountId}/extension/{extensionId}/message-store/{messageId}'].get.responses['207'].schema = doc.definitions.GetMessageMultiResponse
doc.paths['/restapi/v1.0/account/{accountId}/extension/{extensionId}/message-store/{messageId}'].put.responses['207'].schema = doc.definitions.GetMessageMultiResponse
doc.paths['/restapi/v1.0/account/{accountId}/extension/{extensionId}/unified-presence'].get.responses['207'].schema = doc.definitions.UnifiedPresenceList

fs.writeFileSync('rc-platform-adjusted.yml', yaml.safeDump(doc))
