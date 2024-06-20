import request from '@/utils/request'

export function GetList() {
  return request({
    url: '/Lab/Laboratories/GetAllList',
    method: 'get'
  })
}

export function AddLab(data) {
  return request({
    url: '/Lab/Laboratories/AddLab',
    method: 'post',
    data
  })
}

export function EditLab(data) {
  return request({
    url: '/Lab/Laboratories/EditLab',
    method: 'post',
    data
  })
}

export function ChangeState(LID) {
  return request({
    url: '/Lab/Laboratories/ChangeState',
    method: 'post',
    params: { LID }
  })
}

export function GetByFloor(FID) {
  return request({
    url: '/Lab/Laboratories/GetByFloor',
    method: 'get',
    params: { FID }
  })
}
