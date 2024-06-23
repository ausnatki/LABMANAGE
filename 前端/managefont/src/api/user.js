import request from '@/utils/request'

export function login(data) {
  return request({
    url: '/Auth/Auth/GetToken',
    method: 'post',
    data
  })
}

export function getInfo(token) {
  return request({
    url: '/Auth/Auth/info',
    method: 'get',
    params: { token }
  })
}

export function GetAllList() {
  return request({
    url: '/Lab/SysUser/GetAllList',
    method: 'get'
  })
}

export function AddUser(data) {
  return request({
    url: '/Lab/SysUser/AddUser',
    method: 'post',
    data
  })
}

export function EditUser(data) {
  return request({
    url: '/Lab/SysUser/EditUser',
    method: 'post',
    data
  })
}

export function GetRoleList() {
  return request({
    url: '/Lab/SysUser/GetRoleList',
    method: 'get'
  })
}

export function GetByAcademy(CID) {
  return request({
    url: '/Lab/SysUser/GetByAcademy',
    method: 'get',
    params: { CID }
  })
}

export function AssignMent(LID, UID) {
  return request({
    url: '/Lab/Laboratories/AssignMent',
    method: 'post',
    params: { LID, UID }
  })
}

export function GetRepairs() {
  return request({
    url: '/Lab/SysUser/GetRepairs',
    method: 'get'
  })
}

export function GetDash() {
  return request({
    url: '/Lab/SysUser/GetDash',
    method: 'get'
  })
}

// AssignMent
// GetByAcademy
