# Refactoring Learn

*学习参考书籍《重构》*

如果要给程序添加一个特性，但代码因缺乏良好的结构而不易于进行更改，那就先重构那个程序，使其比较容易添加该特性之后再添加该特性

重构的第一步，确保即将修改的代码拥有可靠的测试代码

编程时，需要遵循营地法则，保证你离开时的代码库一定比来时更健康

好代码的检验标准：人们是否能够轻而易举的修改它，一个健康的代码库能够最大程度的提升我们的生产力，支持更快，更低成本的添加新需求

小改动可以更快前进，请保持代码永远处于可工作状态，小步修改累计起来也能大大改善系统的设计

如果有人说他们的代码在重构过程中有一两天时间不可用，基本上可以确定，他们在做的事不是重构

## 重构的原则

### 什么是重构

对程序内部结构进行调整，目的是不改变程序原本行为的前提下，提高其可理解性，降低其修改成本

#### 重构的关键

- 运用大量微小且保持程序原本行为的步骤，一步步达成大规模的修改
- 每个单独的重构要么很小，要么由若干小步骤组合而成
- 重构过程中代码很少进入不可工作状态，即使重构没有完成，也可以在任何时刻停下来

#### 重构与性能优化的相似之处以及区别

相同之处

- 都需要修改代码
- 都不会改变程序的整体功能

区别

- 重构：为了使代码更容易理解，更易于修改，这可能使程序运行的更快，也可能使程序运行的更慢
- 性能优化：只关心让程序运行的更快，最终得到的代码有可能更难理解和维护

### 为什么重构

- 重构有助于代码维持自己该有的形态，改进程序的设计
- 重构可以使程序代码更容易理解
- 重构可以更有效的写出健壮的代码，减少BUG的出现频率
- 重构可以提高编程速度

### 什么时候重构

*三次法则：事不过三，三则重构*

#### 预备性重构

#### 帮助理解的重构

#### 捡垃圾式重构

#### 有计划的重构、见机行事的重构

#### 长期重构

#### 复审代码时重构

### 什么时候不该重构

## 代码的坏味道

## 构筑测试体系

## 介绍重构名录

## 第一组重构

## 封装

## 搬移特性

## 重新组织数据

## 简化条件逻辑

## 重构API

## 处理继承关系
