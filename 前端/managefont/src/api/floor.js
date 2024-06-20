import request from '@/utils/request'

export function GetList() {
  return request({
    url: '/Lab/Floor/GetAllList',
    method: 'get'
  })
}

export function ChangeState(FID) {
  return request({
    url: '/Lab/Floor/ChangeState',
    method: 'post',
    params: { FID }
  })
}

export function AddFloor(BID) {
  return request({
    url: '/Lab/Floor/AddFloor',
    method: 'post',
    params: { BID }
  })
}
