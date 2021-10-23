// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"

#include <functional>
#include "Any.h"
#include "Subsystems/GameInstanceSubsystem.h"
#include "MessageManager.generated.h"

/**
 * 消息管理器：负责各模块之间的通信
 * 1. 注册订阅者（调用函数）：
 * 目前只支持单参数/双参数且返还为void的函数，推荐使用lambda函数
 * 例子：
	std::function<void(int32,int32)> Func = [](int32 arg1,int32 arg2){
		UE_LOG(LogTemp, Warning , TEXT("Receive Message：%d and %d"),arg1,arg2);
	};
	UGameInstance* GameInstance = UGameplayStatics::GetGameInstance(GetWorld());
	UMessageManager* manager = GameInstance->GetSubsystem<UMessageManager>();
	//参数1:订阅频道名，参数2:回调函数，参数3:订阅者ID（默认为0）
	manager->AddListener("test",Func,1);
 * 2. 通知订阅者：
 * 例子：
	UGameInstance* GameInstance = UGameplayStatics::GetGameInstance(GetWorld());
	UMessageManager* manager = GameInstance->GetSubsystem<UMessageManager>();
	//参数1:通知频道名，参数2：回调函数参数，参数3：回调函数参数（若为单参数函数则不写该参数）
	manager->NoticeListener("test",999999999999,2333333333333);
 */
UCLASS()
class GGJ2021_API UMessageManager : public UGameInstanceSubsystem
{
	GENERATED_BODY()
public:
	// 默认创建
	virtual bool ShouldCreateSubsystem(UObject* Outer) const override{ return true; }
	// 析构函数
	virtual ~UMessageManager();
	// 初始化函数
	virtual void Initialize(FSubsystemCollectionBase& Collection)override;

	//添加订阅者（单参数调用）
	template<typename Args1>
    void AddListener(const FName Channel,std::function<void(Args1)> Func, const int32 ID=0) {
		Any a = Func;
		TMultiMap<int32, Any>* v = Listeners.Find(Channel);
		if(!v){
			Listeners.Add(Channel);
		}
		Listeners.Find(Channel)->Add(ID,Any(Func));
	}
	
	//添加订阅者（双参数调用）
	template<typename Args1,typename Args2>
    void AddListener(const FName Channel,std::function<void(Args1,Args2)> Func, const int32 ID=0) {
		Any a = Func;
		TMultiMap<int32, Any>* v = Listeners.Find(Channel);
		if(!v){
			Listeners.Add(Channel);
		}
		Listeners.Find(Channel)->Add(ID,Any(Func));
	}
	
	//移除订阅者
	void RemoveListener(FName Channel,int32 ID = 0);
	
	//通知订阅者（单参数调用）
	template<typename Args1>
    void NoticeListener(FName Channel,Args1 args1)
	{
		auto m = Listeners.Find(Channel);
		for(auto Itr = m->begin();Itr!=m->end();++Itr)
		{
			Itr->Value.cast<std::function<void(Args1)>>()(args1);
		}
	}
	
	//通知订阅者（双参数调用）
	template<typename Args1,typename Args2>
    void NoticeListener(FName Channel,Args1 args1,Args2 args2)
	{
		auto m = Listeners.Find(Channel);
		for(auto Itr = m->begin();Itr!=m->end();++Itr)
		{
			Itr->Value.cast<std::function<void(Args1,Args2)>>()(args1,args2);
		}
	}

private:
	//订阅者
	TMap<FName,TMultiMap<int32,Any>> Listeners;
};
